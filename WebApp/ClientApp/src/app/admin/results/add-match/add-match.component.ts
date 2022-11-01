import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { FormBuilder, Validators } from '@angular/forms';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzDrawerRef } from 'ng-zorro-antd/drawer';

@Component({
  selector: 'add-match-component',
  templateUrl: './add-match.component.html',
  styleUrls: ["./add-match.component.less"]
})
export class AddMatchComponent implements OnInit {
  isLoading = false;
  form = this.fb.group({
    matchdayId: [null, [Validators.required]],
    homeTeamId: [null, [Validators.required]],
    awayTeamId: [null, [Validators.required]],
    matchTimestamp: [null, [Validators.required]],
  });
  matchdays: any;
  teams: any;

  constructor(
    private http: HttpClient,
    private fb: FormBuilder,
    private message: NzMessageService,
    private drawerRef: NzDrawerRef<string>) {
  }

  ngOnInit() {
    this.http.get<any[]>(`${environment.apiUrl}api/Matchdays`).subscribe(result => {
      this.matchdays = result;
    });
    this.http.get<any[]>(`${environment.apiUrl}api/Teams`).subscribe(result => {
      this.teams = result;
    });
  }

  submit() {
    this.isLoading = true;
    console.log(this.form.value);
    this.http.post(`${environment.apiUrl}api/Matches`, this.form.value)
      .subscribe(
        () => {
          this.message.create('success', 'Erfolgreich gespeichert');
          this.drawerRef.close();
        },
        () => {
          this.message.create('error', 'FEHLER');
        },
        () => { this.isLoading = false; }
      );
  }

  close(): void {
    this.drawerRef.close();
  }
}
