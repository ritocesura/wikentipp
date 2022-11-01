import { Component, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormArray, Validators } from '@angular/forms';
import { NzMessageService } from 'ng-zorro-antd/message';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'matchday-tab',
  templateUrl: './matchday-tab.component.html',
  styleUrls: ["./matchday-tab.component.less"]
})
export class MatchdayTabComponent implements OnInit {
  @Input() index = 0;
  disableSave = true;
  curDate = new Date();
  isLoading = false;

  form = this.fb.group({
    matches: this.fb.array([]),
  });

  constructor(
    private http: HttpClient,
    private fb: FormBuilder,
    private message: NzMessageService) {
  }

  ngOnInit() {
    this.http.get<IMatch[]>(`${environment.apiUrl}api/Tipps/own/` + this.index).subscribe(matches => {
      for (var match of matches) {
        const newMatchForm = this.fb.group(match);
        //newMatchForm.controls['awayScoreTipp'].setValidators([conditionalValidator("homeScoreTipp")]);
        //newMatchForm.controls['homeScoreTipp'].setValidators([conditionalValidator("awayScoreTipp")]);

        this.matches.push(newMatchForm);
      }
      if (matches.length > 0) {
        this.disableSave = false;
      }
    });
  }

  get matches(): FormArray {
    return this.form.controls['matches'] as FormArray;
  }

  submit() {
    this.isLoading = true;
    this.http.post(`${environment.apiUrl}api/Tipps`, this.matches.value)
      .subscribe(
        () => {
          this.message.create('success', 'Tipps erfolgreich gespeichert');
          this.form.markAsPristine();
        },
        () => {
          this.message.create('error', 'Tipps konnten nicht erfolgreich gespeichert werden');
        },
        () => { this.isLoading = false; }
      );
  }
}

interface IMatch {
  tippId: number,
  matchId: number,
  matchTimestamp: string,
  homeScoreTipp: number,
  awayScoreTipp: number,
  homeTeamName: string,
  awayTeamName: string,
}
