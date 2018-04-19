import {Component, OnInit} from '@angular/core';
import {MatDialog} from '@angular/material';
import {JournalService, IJournal} from './journal.service';
import {AddJournalDialog} from './AddJournal/add-journal-dialog.component';

@Component({
  selector: 'journal',
  templateUrl: './journal.component.html',
  styleUrls: ['./journal.component.css']
})

export class JournalComponent implements OnInit {

  pageTitle: string = 'Journals';
  journals: IJournal[] = [];
  errorMessage: string;

  constructor(private dialog: MatDialog,
              private journalService: JournalService) {
  }

  ngOnInit(): void {
    this.journalService.getUserJournals()
      .subscribe((result: IJournal[]) => {
        this.journals = result;
      }, (error: any) => {
        this.errorMessage = error;
      });
  }

  createJournal() {

    let dialogRef = this.dialog.open(AddJournalDialog, {
      width: '450px',
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {

    });
  }
}
