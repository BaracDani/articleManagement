import {Component, OnInit} from '@angular/core';
import {MatDialog} from '@angular/material';
import {ArticleService, IArticle} from '../article.service';
import {IJournal, JournalService} from '../../journal/journal.service';
import {ReviewArticleDialog} from './ArticleReviewDialog/review-article-dialog.component';

interface IJournalArticles {
  journal: IJournal;
  articles: IArticle[];
}

@Component({
  selector: 'reviewArticle',
  templateUrl: './review-article.component.html',
  styleUrls: ['./review-article.component.css']
})
export class ReviewArticleComponent implements OnInit {

  pageTitle: string = 'Review articles';
  inReviewArticles: IArticle[] = [];
  errorMessage: string;

  constructor(private dialog: MatDialog,
              private articleService: ArticleService) {
  }

  ngOnInit(): void {
    this.getUserToReviewArticles();
  }

  getUserToReviewArticles(): void {
    this.articleService.getUserToReviewArticles().subscribe((result: IArticle[]) => {
      this.inReviewArticles = result;
    }, (error: any) => {
      this.errorMessage = error;
    });
  }

  reviewArticle(article: IArticle): void {

    let dialogRef = this.dialog.open(ReviewArticleDialog, {
      width: '550px',
      data: article
    });

    dialogRef.afterClosed().subscribe(result => {
      this.getUserToReviewArticles();
    });
  }
  abstain(article: IArticle): void {
    this.articleService.abstainReviewArticle(article).subscribe((result: any) => {
      this.getUserToReviewArticles();
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
