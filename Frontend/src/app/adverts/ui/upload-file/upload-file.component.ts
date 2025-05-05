import { Component, EventEmitter, Input, Output } from '@angular/core';

import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzUploadChangeParam, NzUploadFile, NzUploadModule, UploadFileStatus } from 'ng-zorro-antd/upload';

@Component({
  selector: 'app-upload-file',
  imports: [NzButtonModule, NzIconModule, NzUploadModule],
  templateUrl: './upload-file.component.html',
  styleUrl: './upload-file.component.css'
})
export class UploadFileComponent {

  @Input() disabled: boolean = false;
  @Output() fileListChange = new EventEmitter<NzUploadFile[]>();

  fileList: NzUploadFile[] = [];
  isUploading: boolean = false;

  handleChange(info: NzUploadChangeParam): void {
    const existingFileIndex = this.fileList.findIndex(file => file.uid === info.file.uid);
  
    if (existingFileIndex === -1) {
      const uploadedFile: NzUploadFile = {
        uid: `${info.file.uid}`, 
        name: info.file.name,
        status: 'uploading',
        url: ''
      };
  
      this.fileList = [...this.fileList, uploadedFile];
      this.fileListChange.emit(this.fileList);
    } else {
      const updatedFileList = this.fileList.map(file => {
        if (file.uid === info.file.uid) {
          return {
            ...file,
            status: info.file.status,
            url: info.file.response ? info.file.response.big.url : file.url,
            id: info.file.response.id
          };
        }
        return file;
      });
  
      this.fileList = updatedFileList;
      this.fileListChange.emit(this.fileList);
    }
  }
  
}
