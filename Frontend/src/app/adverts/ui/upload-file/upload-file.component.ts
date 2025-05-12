import { Component, EventEmitter, Input, Output } from '@angular/core';

import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzUploadChangeParam, NzUploadFile, NzUploadModule, UploadFileStatus } from 'ng-zorro-antd/upload';
import { environment } from '../../../../environments/environment.development';

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

  handleChange({ fileList }: NzUploadChangeParam): void {
    this.fileListChange.emit(fileList);
  }
  
}
