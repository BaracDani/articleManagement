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
  articles: IArticle[] = [];
  errorMessage: string;

  constructor(private dialog: MatDialog,
              private articleService: ArticleService) {
  }

  ngOnInit(): void {
    this.articleService.getUserArticles()
      .subscribe((result: IArticle[]) => {
        this.articles = result;
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

    });
  }
}
