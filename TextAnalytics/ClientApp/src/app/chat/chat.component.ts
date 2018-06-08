import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HubConnection } from '@aspnet/signalr';
import { Document } from '../models/document';
import { DocumentResult } from '../models/documentResult';
import * as signalR from  '@aspnet/signalr';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  public user: string;
  public message: string;
  public messages: Document[];
  public language: string;
  public isUserPresent: boolean;
  private hubConnection: HubConnection;
  private counter: number;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.message = '';
    this.language = '';
    this.messages = [];
    this.counter = 1;
    this.isUserPresent = false;
  }

  ngOnInit() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('/chat')
      .build();

    this.hubConnection.on('ReceiveMessage', (user: string, message: string) => {
      const msg = message
        .replace(/&/g, '&amp;')
        .replace(/</g, '&lt;')
        .replace(/>/g, '&gt;');
      const encodedMsg = `${user}: ${msg}`;
      ++this.counter;
      this.messages.push({id: this.counter.toString(), language: this.language, message: encodedMsg, text: msg, score: 0, scored: false});
    });

    this.hubConnection.start().catch(err => console.error(err.toString()));
  }

  public sendMessage(): void {
    this.hubConnection
      .invoke('SendMessage', this.user, this.message)
      .then(() => (this.message = ''))
      .catch(err => console.error(err));
  }

  public continue(): void {
    if (this.user && this.language) {
      this.isUserPresent = true;
    }
  }

  public score(): void {
    const elements = this.messages.filter(document => !document.scored);
    this.http.post<DocumentResult[]>(`${this.baseUrl}/api/textanalytics/sentiment`, elements).subscribe(result => {
      result.forEach((value, index, array) => {
        var message = this.messages.find(element => element.id === value.id);
        message.scored = true;
        message.score = value.score;
      });
    }, error => console.error(error));
    console.log('scored');
  }

  public getClass(document: Document): string {
    if (!document.scored) {
      return '';
    }

    if (document.score > 0.5) {
      return 'positive';
    }

    return 'negative';
  }
}
