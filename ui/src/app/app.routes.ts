import { Routes } from '@angular/router';
import {Home} from './home/home';
import {Duty} from './duty/duty';

export const routes: Routes = [{
  path: '',
  title: 'Home',
  component: Home
  },
  {
    path: 'duty',
    title: 'Duty',
    component: Duty
  }];
