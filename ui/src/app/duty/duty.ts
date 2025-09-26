import {Component, inject} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {AstronautDuty, AstronautDutiesResponse} from '../models/duties';
import {Person} from '../models/person';

@Component({
  selector: 'app-duty',
  imports: [
    ReactiveFormsModule,
    FormsModule
  ],
  templateUrl: './duty.html',
  styleUrl: './duty.css'
})
export class Duty {
  private http = inject(HttpClient);
  searchName = ''
  duties: AstronautDuty[] = [];
  person: Person | null = null;

  onSubmit() {
    this.http.get<AstronautDutiesResponse>('https://localhost:7204/AstronautDuty/' + this.searchName).subscribe(data => this.duties = data.astronautDuties);
  }
}
