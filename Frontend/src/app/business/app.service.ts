import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AppService {
  pageTitle$ = new BehaviorSubject<string>('');
  isLoading$ = new BehaviorSubject<boolean>(false);
}
