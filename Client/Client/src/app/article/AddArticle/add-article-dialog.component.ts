import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {FormControl} from '@angular/forms';
import {Observable} from 'rxjs/Observable';
import {startWith} from 'rxjs/operators/startWith';
import {map} from 'rxjs/operators/map';
import {ArticleService, IArticle} from '../article.service';

@Component({
  selector: 'add-article-dialog',
  templateUrl: './add-article-dialog.component.html',
  styleUrls: ['./add-article-dialog.component.css']
})
export class AddArticleDialog {
  errorMessage: string;
  title: string;
  abstract: string;
  author: string;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
              private dialogRef: MatDialogRef<AddArticleDialog>,
              private articleService: ArticleService) { }

  onCancel(): void {
    this.dialogRef.close();
  }

  onAddArticle(): void {
    let data: IArticle = {
      title: this.title,
      abstract: this.abstract,
      author: this.author
    };

    this.articleService.createArticle(data).subscribe((_) => {

      this.dialogRef.close();
    }, (error: any) => {
      this.errorMessage = error && error.messageDetail ? error.messageDetail : error;
    });
  }

}
