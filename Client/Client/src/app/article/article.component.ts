import {Component, OnInit} from '@angular/core';
import {MatDialog} from '@angular/material';
import {AddArticleDialog} from './AddArticle/add-article-dialog.component';
import {ArticleService, IArticle} from './article.service';

@Component({
  selector: 'article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.css']
})

export class ArticleComponent implements OnInit {

  pageTitle: string = 'Article';
  approvedArticles: IArticle[] = [];
  rejectedArticles: any[] = [];
  pendingArticles: IArticle[] = [];
  inReviewArticles: IArticle[] = [];
  errorMessage: string;

  constructor(private dialog: MatDialog,
              private articleService: ArticleService) {
  }

  ngOnInit(): void {
    this.getArticles();
  }

  getArticles() {
    this.articleService.getUserArticles()
      .subscribe((result: IArticle[]) => {
        this.approvedArticles = result.filter((article)=> article.approvalStatus === 2);
        this.rejectedArticles = result.filter((article)=> article.approvalStatus === 3);
        this.inReviewArticles = result.filter((article)=> article.approvalStatus === 4);
        this.pendingArticles = result.filter((article)=> article.approvalStatus === 1);
        this.rejectedArticles.forEach((article) => {
          this.articleService.getReviewsForArticle(article).subscribe((reviews: any[]) => {
            article.reviews = reviews.filter((review) => !!review);
          }, (error: any) => {
            this.errorMessage = error;
          });
        });

      }, (error: any) => {
        this.errorMessage = error;
      });
  }

  createArticle() {

    let dialogRef = this.dialog.open(AddArticleDialog, {
      width: '450px',
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      this.getArticles();
    });
  }



  downloadFile(filePath: string) {
    this.articleService.getFile(filePath)
      .subscribe((result: any) => {
        console.log(result);
        let a = document.createElement("a");
        a.href = URL.createObjectURL(result);
        if (result.type === "application/pdf"){
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
