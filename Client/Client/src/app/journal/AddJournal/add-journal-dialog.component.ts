import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {FormControl, Validators} from '@angular/forms';
import {Observable} from 'rxjs/Observable';
import {startWith} from 'rxjs/operators/startWith';
import {map} from 'rxjs/operators/map';
import {JournalService, IAddJournal, IDomain} from '../journal.service';

@Component({
  selector: 'add-journal-dialog',
  templateUrl: './add-journal-dialog.component.html',
  styleUrls: ['./add-journal-dialog.component.css']
})
export class AddJournalDialog {
  domainControl: FormControl = new FormControl('', [Validators.required]);
  domains: IDomain[] = [];
  errorMessage: string;
  title: string;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
              private dialogRef: MatDialogRef<AddJournalDialog>,
              private journalService: JournalService) {
  }

  ngOnInit() {

    this.journalService.getDomains()
      .subscribe((result: IDomain[]) => {
        this.domains = result;
      }, (error: any) => {
        this.errorMessage = error;
      });
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onAddJournal(): void {
    let data: IAddJournal = {
      title: this.title,
      domainId: this.domainControl.value.id
    };

    this.journalService.createJournal(data).subscribe((_) => {

      this.dialogRef.close();
    }, (error: any) => {
      this.errorMessage = error && error.messageDetail ? error.messageDetail : error;
    });
  }
}
