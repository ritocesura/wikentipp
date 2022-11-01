import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-betting-overview-component',
  templateUrl: './betting-overview.component.html',
  styleUrls: ["./betting-overview.component.less"]
})
export class BettingOverviewComponent {
  public overview: any;
  public matchdays: any;
  public selectedMatchday: string = 'Spieltag 1';

  constructor(http: HttpClient) {
    http.get<object[]>(`${environment.apiUrl}api/Tipps/overviewPerMatchday`).subscribe(result => {
      this.overview = result;
    }, error => console.error(error));

    http.get<object[]>(`${environment.apiUrl}api/Matchdays`).subscribe(result => {
      this.matchdays = result;
    }, error => console.error(error));
  }
}
