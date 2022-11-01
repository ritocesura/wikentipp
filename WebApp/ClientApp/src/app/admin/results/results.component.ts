import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { FormBuilder, FormArray } from '@angular/forms';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzDrawerRef, NzDrawerService } from 'ng-zorro-antd/drawer';
import { AddMatchComponent } from './add-match/add-match.component';

@Component({
  selector: 'app-results-component',
  templateUrl: './results.component.html',
  styleUrls: ["./results.component.less"]
})
export class ResultsComponent implements OnInit {
  isLoading = false;
  form = this.fb.group({
    matches: this.fb.array([]),
  });

  constructor(
    private http: HttpClient,
    private fb: FormBuilder,
    private message: NzMessageService,
    private drawerService: NzDrawerService  ) {
  }

  ngOnInit() {
    this.http.get<any[]>(`${environment.apiUrl}api/Matches`).subscribe(matches => {
      for (var match of matches) {
        this.matches.push(this.fb.group(match));
      }
    });
  }

  get matches(): FormArray {
    return this.form.controls['matches'] as FormArray;
  }

  submit() {
    this.isLoading = true;
    this.http.put(`${environment.apiUrl}api/Matches/scores`, this.matches.value)
      .subscribe(
        () => {
          this.message.create('success', 'Erfolgreich gespeichert');
          this.form.markAsPristine();
        },
        () => {
          this.message.create('error', 'FEHLER');
        },
        () => { this.isLoading = false; }
      );
  }
  delete(id: number) {
    const item = this.matches.at(id).value;
    this.http.delete(`${environment.apiUrl}api/Matches/` + item.id)
      .subscribe(
        () => {
          this.message.create('success', 'Erfolgreich gelöscht');
          this.matches.removeAt(id);
        },
        () => {
          this.message.create('error', 'FEHLER');
        },
        () => { this.isLoading = false; }
      );
  }

  open(): void {
    const drawerRef = this.drawerService.create<AddMatchComponent, { value: string }, string>({
      nzTitle: 'New Match',
      nzContent: AddMatchComponent,
      nzMaskClosable: false,
      nzWidth: 600
    });

    drawerRef.afterClose.subscribe(() => {
    });
  }
}
