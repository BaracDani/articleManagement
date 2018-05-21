import {Component, OnInit} from '@angular/core';
import {MatDialog} from '@angular/material';
import {ArticleService, IArticle} from '../article.service';
import {IJournal, JournalService} from '../../journal/journal.service';
import {IUserModel} from "../../admin/admin.model";

interface IJournalArticles {
  journal: IJournal;
  articles: IArticle[];
}

@Component({
  selector: 'review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.css']
})
export class ReviewComponent implements OnInit {

  pageTitle: string = 'Review journals';
  journalArticles: IJournalArticles[] = [];
  errorMessage: string;

  constructor(private dialog: MatDialog,
              private articleService: ArticleService,
              private journalService: JournalService) {
  }

  ngOnInit(): void {
    this.getUnpublishedUserJournals();
  }

  getUnpublishedUserJournals(): void {
    this.journalArticles = [];
    this.journalService.getUnpublishedUserJournals()
      .subscribe((result: IJournal[]) => {
        result.forEach((journal) => {
          this.articleService.getPendingArticles(journal.id)
            .subscribe((result: IArticle[]) => {
              let journalArticle = {
                journal: journal,
                articles: result
              };
              this.journalArticles.push(journalArticle);
            }, (error: any) => {
              this.errorMessage = error;
            });
        });
      }, (error: any) => {
        this.errorMessage = error;
      });
  }

  approveArticle(article: IArticle, journal: IJournal): void {
    let reviewers: IUserModel[] = [];
    this.articleService.getUsersOfDomain(journal.domainId, article.userId).subscribe((users: any) => {
      reviewers = users;

      let userIds: string[] = reviewers.map((user) => user.id);
      this.articleService.approveArticle(article, userIds).subscribe((result: any) => {
        this.getUnpublishedUserJournals();
      }, (error: any) => {
        this.errorMessage = error;
      });
    }, (error: any) => {
      this.errorMessage = error;
    });
  }

  rejectArticle(article: IArticle): void {
    this.articleService.rejectArticle(article).subscribe((result: any) => {
      this.getUnpublishedUserJournals();
    }, (error: any) => {
      this.errorMessage = error;
    });
  }

  downloadFile(filePath: string) {
    this.articleService.getFile(filePath)
      .subscribe((result: any) => {
        console.log(result);
        let a = document.createElement("a");
        a.href = URL.createObjectURL(result);
        if (result.type === "application/pdf") {
          a.download = 'download.pdf';
        }
        if (result.type === "application/msword") {
          a.download = 'download.doc';
        }
        if (result.type ===
          "application/vnd.openxmlformats-officedocument.wordprocessingml.document") {
          a.download = 'download.docx';
        }
        document.body.appendChild(a);
        a.click();
        //var fileURL = URL.createObjectURL(blob);
        //window.open(fileURL);
      }, (error: any) => {
        this.errorMessage = error;
      });
  }

}
