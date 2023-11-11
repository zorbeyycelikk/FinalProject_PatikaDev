import { Component } from '@angular/core';
import { LoaderService } from '../services/loader.service';
import { Observable, map } from 'rxjs';

@Component({
  selector: 'app-spinners',
  templateUrl: './spinners.component.html',
  styleUrls: ['./spinners.component.css']
})
export class SpinnersComponent {

  public showSpinner$ : Observable<any>

  constructor(loaderService : LoaderService){

    this.showSpinner$ = loaderService.pendingHttpRequest$
    .pipe(map(pendingHttpRequests => {
      return pendingHttpRequests > 0
    }))
    
  }
}
