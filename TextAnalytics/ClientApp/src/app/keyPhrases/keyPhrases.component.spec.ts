import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { KeyPhrasesComponent } from './keyPhrases.component';

describe('KeyphrasesComponent', () => {
  let component: KeyPhrasesComponent;
  let fixture: ComponentFixture<KeyPhrasesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ KeyPhrasesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(KeyPhrasesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
