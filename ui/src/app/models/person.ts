class Person {
  name: string;
  personId: number;
  currentRank: string;
  currentDutyTitle: string;
  careerStartDate: Date;
  careerEndDate: Date;

  constructor(name: string, personId: number, currentRank: string, currentDutyTitle: string, careerStartDate: Date, careerEndDate: Date) {
    this.name = name;
    this.personId = personId;
    this.currentRank = currentRank;
    this.currentDutyTitle = currentDutyTitle;
    this.careerStartDate = careerStartDate;
    this.careerEndDate = careerEndDate;
  }
}

class PersonResponse {
  people: Person[];
  success: boolean;
  message: string;
  responseCode: number;

  constructor(people: Person[], success: boolean, message: string, responseCode: number) {
    this.people = people;
    this.success = success;
    this.message = message;
    this.responseCode = responseCode;
  }
}

export {Person, PersonResponse};
