import {Component, inject} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {Person, PersonResponse} from '../models/person';

@Component({
  selector: 'app-home',
  imports: [
    ReactiveFormsModule,
    FormsModule
  ],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home {
  private http = inject(HttpClient);
  searchName = ''
  people: Person[] = [];

  onSubmit() {
    this.http.get<PersonResponse>('https://localhost:7204/Person').subscribe(people => this.people = people.people);
  }
}
