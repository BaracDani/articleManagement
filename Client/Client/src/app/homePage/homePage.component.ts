import {Component} from '@angular/core';
import {ArticleService, IArticle} from '../article/article.service';
import {IJournal, JournalService} from '../journal/journal.service';

@Component({
  selector: 'homePage',
  templateUrl: './homePage.component.html',
  styleUrls: ['./homePage.component.css']
})

export class HomePageComponent {

  pageTitle: string = 'HomePage';
  journals: IJournal[] = [];
  errorMessage: string;

  constructor(private articleService: ArticleService,
              private journalService: JournalService) {
  }


  ngOnInit(): void {
    this.journalService.getJournals()
      .subscribe((result: IJournal[]) => {
        this.journals = result;
      }, (error: any) => {
        this.errorMessage = error;
      });
  }


}
