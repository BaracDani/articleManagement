import {Component, OnInit} from '@angular/core';
import {MatDialog} from '@angular/material';
import {ArticleService, IArticle} from '../article.service';
import {IJournal, JournalService} from "../../journal/journal.service";

@Component({
  selector: 'review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.css']
})

export class ReviewComponent implements OnInit {

  pageTitle: string = 'Review articles';
  pendingArticles: IArticle[] = [];
  errorMessage: string;

  constructor(private dialog: MatDialog,
              private articleService: ArticleService,
              private journalService: JournalService) {
  }

  ngOnInit(): void {

    this.journalService.getUserJournals()
      .subscribe((result: IJournal[]) => {
        result.forEach((journal) => {
          this.articleService.getPendingArticles(journal.id)
            .subscribe((result: IArticle[]) => {
              this.pendingArticles.push(... result);
            }, (error: any) => {
              this.errorMessage = error;
            });
        });
      }, (error: any) => {
        this.errorMessage = error;
      });
  }

  approveArticle(article: IArticle): void {
    this.articleService.approveArticle(article).subscribe((result: any) => {
      console.log(result);
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
