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
  userJournals: IJournal[] = [];
  errorMessage: string;

  constructor(private dialog: MatDialog,
              private journalService: JournalService) {
  }

  ngOnInit(): void {
    this.getJournals();
  }

  getJournals() {
    this.journalService.getUserJournals()
      .subscribe((result: IJournal[]) => {
        this.userJournals = result;
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
      this.getJournals();
    });
  }

  publishJournal(journal: IJournal) {
    journal.isPublished = true;
    journal.publishDate = new Date().toISOString();
    this.journalService.updateJournal(journal).subscribe((result: any) => {
    }, (error: any) => {
      this.errorMessage = error;
    });
  }

  unpublishJournal(journal: IJournal) {
    journal.isPublished = false;
    this.journalService.updateJournal(journal).subscribe((result: any) => {
    }, (error: any) => {
      this.errorMessage = error;
    });
  }

  deleteJournal(journal: IJournal) {
    this.journalService.deleteJournal(journal).subscribe((result: any) => {
      this.getJournals();
    }, (error: any) => {
      this.errorMessage = error;
    });
  }
}
