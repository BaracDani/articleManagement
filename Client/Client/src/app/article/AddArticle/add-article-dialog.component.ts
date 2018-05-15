import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {FormControl, Validators} from '@angular/forms';
import {Observable} from 'rxjs/Observable';
import {startWith} from 'rxjs/operators/startWith';
import {map} from 'rxjs/operators/map';
import {ArticleService, IAddArticle, IArticle} from '../article.service';
import {IDomain, IJournal, JournalService} from '../../journal/journal.service';

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
  selectedFile: File;

  journalsControl: FormControl = new FormControl('', [Validators.required]);
  journals: IJournal[] = [];

  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
              private dialogRef: MatDialogRef<AddArticleDialog>,
              private articleService: ArticleService,
              private journalService: JournalService) {
  }

  ngOnInit() {

    this.journalService.getJournals()
      .subscribe((result: IJournal[]) => {
        this.journals = result;
      }, (error: any) => {
        this.errorMessage = error;
      });
  }


  onCancel(): void {
    this.dialogRef.close();
  }

  onAddArticle(): void {
    if (this.errorMessage) {
      return;
    }

    let formData: FormData = new FormData();
    formData.append('uploadFile', this.selectedFile, this.selectedFile.name);
    formData.append('title', this.title);
    formData.append('abstract', this.abstract);
    formData.append('author', this.author);
    formData.append('journalId',this.journalsControl.value.id)

    this.articleService.createArticle(formData).subscribe((_) => {

      this.dialogRef.close();
    }, (error: any) => {
      //this.errorMessage = error && error.messageDetail ? error.messageDetail : error;
      this.errorMessage = 'Server error';
    });
  }

  fileChange(event) {
    let fileList: FileList = event.target.files;
    if (fileList.length > 0) {
      let file: File = fileList[0];

      if (file.type !== "application/pdf" && file.type !== "application/msword" && file.type !==
        "application/vnd.openxmlformats-officedocument.wordprocessingml.document") {
        this.errorMessage = 'Invalid file format. Please upload .pdf, .doc or .docx';
      } else {
        this.errorMessage = '';
        this.selectedFile = file;
      }
    }
  }

}
