import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {

  public pendingHttpRequest$: BehaviorSubject<number> 

  constructor() {
    this.pendingHttpRequest$ = new BehaviorSubject(0);
   }
   requestStarted(){
    this.pendingHttpRequest$.next(this.pendingHttpRequest$.getValue() + 1)
   }

   requestFinished(){
    this.pendingHttpRequest$.next(this.pendingHttpRequest$.getValue() - 1)
   }
}
