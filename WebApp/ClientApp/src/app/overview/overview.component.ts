import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-overview-component',
  templateUrl: './overview.component.html',
  styleUrls: ["./overview.component.less"]
})
export class OverviewComponent {
  public overview: any;

  constructor(http: HttpClient) {
    http.get<object[]>(`${environment.apiUrl}api/Tipps/overview`).subscribe(result => {
      this.overview = result;
    }, error => console.error(error));
  }
}
