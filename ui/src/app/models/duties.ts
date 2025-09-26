import {Person} from './person';

class AstronautDuty {
  id: number;
  rank: string;
  personId: number;
  dutyStartDate: Date;
  dutyEndDate: Date;
  dutyTitle: string;

  constructor(id: number, rank: string, personId: number, dutyStartDate: Date, dutyEndDate: Date, dutyTitle: string) {
    this.id = id;
    this.rank = rank;
    this.personId = personId;
    this.dutyStartDate = dutyStartDate;
    this.dutyEndDate = dutyEndDate;
    this.dutyTitle = dutyTitle;
  }
}

class AstronautDutiesResponse {
  astronautDuties: AstronautDuty[];
  success: boolean;
  message: string;
  responseCode: number;

  constructor(duties: AstronautDuty[], success: boolean, message: string, responseCode: number) {
    this.astronautDuties = duties;
    this.success = success;
    this.message = message;
    this.responseCode = responseCode;
  }
}

export {AstronautDuty, AstronautDutiesResponse};
