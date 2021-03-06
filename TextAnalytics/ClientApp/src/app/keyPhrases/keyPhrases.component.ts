import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HubConnection } from '@aspnet/signalr';
import { KeyPhrase } from '../models/keyPhrase';
import * as signalR from '@aspnet/signalr';

@Component({
  selector: 'app-keyPhrases',
  templateUrl: './keyPhrases.component.html',
  styleUrls: ['./keyPhrases.component.css']
})
export class KeyPhrasesComponent implements OnInit {
  public uploadSaveUrl; // should represent an actual API endpoint
  public fileNames: string[];
  public phrases: string[];
  private hubConnection: HubConnection;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.uploadSaveUrl = `${baseUrl}/api/files/upload`;
    this.phrases = [''];
  }

  ngOnInit() {
    this.http.get<string[]>(`${this.baseUrl}/api/files`).subscribe(result => {
      this.fileNames = result;
    });

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('/file')
      .build();

    this.hubConnection.on('ReceiveFile', (fileName: string) => {
      this.fileNames.push(fileName);
    });

    this.hubConnection.start().catch(err => console.error(err.toString()));
  }

  public getPhrases(fileName: string) {
    this.http.get<KeyPhrase>(`${this.baseUrl}/api/files/${fileName}`).subscribe(result => {
      this.phrases = result.keyPhrases.split(',');
    });
  }

}
