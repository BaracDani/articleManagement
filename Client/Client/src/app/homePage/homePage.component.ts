import {Component} from '@angular/core';
import {ArticleService, IArticle} from "../article/article.service";

@Component({
  selector: 'homePage',
  templateUrl: './homePage.component.html',
  styleUrls: ['./homePage.component.css']
})

export class HomePageComponent {

  pageTitle: string = 'HomePage';
  articles: IArticle[] = [];
  errorMessage: string;

  constructor(private articleService: ArticleService) {
  }


  ngOnInit(): void {
    this.articleService.getArticles()
      .subscribe((result: IArticle[]) => {
        this.articles = result;
      }, (error: any) => {
        this.errorMessage = error;
      });
  }

}
