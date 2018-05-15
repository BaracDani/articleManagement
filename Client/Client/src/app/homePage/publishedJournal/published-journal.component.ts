import {Component, Input} from '@angular/core';
import {IJournal, JournalService} from '../../journal/journal.service';
import {ArticleService, IArticle} from '../../article/article.service';

@Component({
  selector: 'publishedJournal',
  templateUrl: './published-journal.component.html',
  styleUrls: ['./published-journal.component.css']
})

export class PublishedJournalComponent {

  @Input() journal: IJournal;
  articles: IArticle[] = [];
  errorMessage: string;

  constructor(private journalService: JournalService,
              private articleService: ArticleService) {
  }

  ngOnInit(): void {
    this.journalService.getJournalArticles(this.journal.id)
      .subscribe((result: IArticle[]) => {
        this.articles = result;
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
