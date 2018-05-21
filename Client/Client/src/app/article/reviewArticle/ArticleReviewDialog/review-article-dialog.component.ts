import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {Observable} from 'rxjs/Observable';
import {startWith} from 'rxjs/operators/startWith';
import {map} from 'rxjs/operators/map';
import {ArticleService, IArticle} from '../../article.service';

@Component({
  selector: 'review-article-dialog',
  templateUrl: './review-article-dialog.component.html',
  styleUrls: ['./review-article-dialog.component.css']
})
export class ReviewArticleDialog {
  errorMessage: string;
  comment: string;
  reviewPoints: number;

  constructor(@Inject(MAT_DIALOG_DATA) public data: IArticle,
              private dialogRef: MatDialogRef<ReviewArticleDialog>,
              private articleService: ArticleService) {
  }

  ngOnInit() {
  }


  onCancel(): void {
    this.dialogRef.close();
  }

  onReviewArticle(): void {
    this.articleService.reviewArticle(this.data, this.reviewPoints, this.comment).subscribe((result: any) => {

      this.dialogRef.close();
    }, (error: any) => {
      this.errorMessage = error;
    });
  }


}
