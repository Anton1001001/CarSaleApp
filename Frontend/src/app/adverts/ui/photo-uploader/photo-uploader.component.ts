import { Component, EventEmitter, Output } from '@angular/core';
import {
  CdkDrag,
  CdkDragDrop,
  CdkDropList,
  moveItemInArray,
} from "@angular/cdk/drag-drop";
import { UploadFileComponent } from "../upload-file/upload-file.component";
import { NzUploadFile } from 'ng-zorro-antd/upload';
import { CommonModule } from '@angular/common';
import { NzSpinModule } from 'ng-zorro-antd/spin'; 
import { NzIconModule } from 'ng-zorro-antd/icon';
import { PhotoRequest } from '../../data-access/models/photo-request';

@Component({
  selector: 'app-photo-uploader',
  imports: [CommonModule, CdkDropList, CdkDrag, UploadFileComponent, NzSpinModule, NzIconModule],
  templateUrl: './photo-uploader.component.html',
  styleUrl: './photo-uploader.component.css'
})
export class PhotoUploaderComponent {

  items: NzUploadFile[] = [];
  photoRequest: PhotoRequest | null = null;
  @Output() photoRequestChange = new EventEmitter<PhotoRequest>();

  private updatePhotoRequest(fileList: NzUploadFile[]) {
    if (!this.checkAllFilesDone(fileList)) return;
    
    this.photoRequest = {
      files: fileList.map(file => file.response.id),
      main: fileList[0].response.id
    };
    this.photoRequestChange.emit(this.photoRequest);
  }

  drop(event: CdkDragDrop<string[]>) {
    moveItemInArray(this.items, event.previousIndex, event.currentIndex);
    this.updatePhotoRequest(this.items);
  }

  updateFileList(fileList: NzUploadFile[]) {
    this.items = fileList;
    this.updatePhotoRequest(this.items);
  }

  onDragStart() {
    document.body.style.cursor = 'grabbing';
  }
  
  onDragEnd() {
    document.body.style.cursor = 'default';
  }

  private checkAllFilesDone(fileList: NzUploadFile[]): boolean {
    return fileList.length > 0 && fileList.every(file => file.status === 'done');
  }
  
}


