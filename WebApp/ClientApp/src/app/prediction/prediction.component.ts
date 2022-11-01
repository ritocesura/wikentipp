import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup } from '@angular/forms';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-prediction-component',
  templateUrl: './prediction.component.html',
  styleUrls: ["./prediction.component.less"]
})
export class PredictionComponent {
  public matchdays: Matchday[] = [];
  public form!: FormGroup;
  public tabIndex = 0;

  constructor(http: HttpClient) {
    http.get<Matchday[]>(`${environment.apiUrl}api/Matchdays`).subscribe(result => {
      this.matchdays = result;

      // set tabIndex depending on todays date
      const currDate = new Date().setUTCHours(0, 0, 0, 0)
      this.matchdays.forEach((value, index) => {
        var matchDayDate = new Date(value.date).setUTCHours(0, 0, 0, 0)
        if (matchDayDate === currDate) {
          this.tabIndex = index;
        }
      });
    }, error => console.error(error));
  }
}

export interface Matchday {
  id: number;
  name: string;
  date: Date;
}
