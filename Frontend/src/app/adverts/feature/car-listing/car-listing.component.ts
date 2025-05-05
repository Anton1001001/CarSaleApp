import { Component, Directive, inject , OnInit} from '@angular/core';
import { ReactiveFormsModule, FormsModule, FormBuilder, Validators, FormControl, FormArray, FormGroup } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzRadioModule } from 'ng-zorro-antd/radio';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { PhotoUploaderComponent } from "../../ui/photo-uploader/photo-uploader.component";
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { PhotoRequest } from '../../data-access/models/photo-request';
import { AdvertService, GetCarAdvertFormResponse } from '../../data-access/advert.service';
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'app-car-listing',
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NzButtonModule,
    NzFormModule,
    NzInputModule,
    NzSelectModule,
    NzRadioModule,
    NzListModule,
    NzIconModule,
    PhotoUploaderComponent,
    NzTypographyModule
],
  templateUrl: './car-listing.component.html',
  styleUrls: ['./car-listing.component.css']
})
export class CarListingComponent implements OnInit {

  isModelVisible: boolean = false;
  isYearVisible: boolean = false;
  isGenerationVisible: boolean = false;
  isBodyTypeVisible: boolean = false;
  isTransmissionTypeVisible: boolean = false;
  isEngineTypeVisible: boolean = false;
  isDriveTypeVisible: boolean = false;
  isModificationVisible: boolean = false;
  isPlaceCityVisible: boolean = false;

  private fb = inject(FormBuilder);
  private advertService = inject(AdvertService);
  private message = inject(NzMessageService);

  validateForm = this.fb.group({
    Params: this.fb.group({
      brandId: this.fb.control<number | null>(null, Validators.required),
      modelId: this.fb.control<number | null>(null, Validators.required),
      mileage: this.fb.control<number | null>(null, Validators.required),
      mileageUnit: this.fb.control<number>(1),
      year: this.fb.control<number | null>(null, Validators.required),
      generationId: this.fb.control<number | null>(null, Validators.required),
      bodyTypeId: this.fb.control<number | null>(null, Validators.required),
      transmissionTypeId: this.fb.control<number | null>(null, Validators.required),
      engineTypeId: this.fb.control<number | null>(null, Validators.required),
      driveTypeId: this.fb.control<number | null>(null, Validators.required),
      modificationId: this.fb.control<number | null>(null, Validators.required),
      conditionId: this.fb.control<number | null>(null, Validators.required),
      colorId: this.fb.control<number | null>(null, Validators.required),
      interiorMaterialId: this.fb.control<number | null>(null, Validators.required),
      interiorColorId: this.fb.control<number | null>(null, Validators.required),
      registrationCountryId: this.fb.control<number | null>(null, Validators.required),
      registrationStatus: this.fb.control<number | null>(null, Validators.required),
      description: this.fb.control<string | null>(null, Validators.required),
      price: this.fb.control<number | null>(null, Validators.required),
      currency: this.fb.control<number | null>(1, Validators.required),
      placeRegionId: this.fb.control<number | null>(null, Validators.required),
      placeCityId: this.fb.control<number | null>(null, Validators.required),
      sellerName: this.fb.control<string | null>(null, Validators.required),
      phones: this.fb.array([this.createPhoneFormGroup()], Validators.required),
      photos: this.fb.group({
        files: this.fb.control<number[]>([], Validators.required),
        main: this.fb.control<number | null>(null, Validators.required)
      })
    })
  });
  
  async onSubmit(): Promise<void> {

  }

  advertFormData: GetCarAdvertFormResponse| null = null;

  resetFields(fields: string[]) {
    const paramsGroup = this.validateForm.get('Params') as FormGroup;  
    fields.forEach(field => paramsGroup.get(field)?.reset());
  }

  onPhotoRequestChange(photoRequest: PhotoRequest) {
    const photosGroup = this.validateForm.get('Params.photos') as FormGroup;
    photosGroup.patchValue({
      files: photoRequest.files,
      main: photoRequest.main
    });

    console.log(this.validateForm);
  }
  
  
  onBrandChange(selectedValue: any) {
    this.isModelVisible = selectedValue !== null;
    this.isYearVisible = false;
    this.isGenerationVisible = false;
    this.isBodyTypeVisible = false;
    this.isTransmissionTypeVisible = false;
    this.isEngineTypeVisible = false;
    this.isDriveTypeVisible = false;
    this.isModificationVisible = false;

    this.resetFields([
      'modelId', 
      'year', 
      'generationId', 
      'bodyTypeId', 
      'transmissionTypeId', 
      'engineTypeId', 
      'driveTypeId', 
      'modificationId'
    ])
  }

  onModelChange(selectedValue: any) {
    this.isYearVisible = selectedValue !== null;
    this.isGenerationVisible = false;
    this.isBodyTypeVisible = false;
    this.isTransmissionTypeVisible = false;
    this.isEngineTypeVisible = false;
    this.isDriveTypeVisible = false;
    this.isModificationVisible = false;

    this.resetFields([ 
      'year', 
      'generationId', 
      'bodyTypeId', 
      'transmissionTypeId', 
      'engineTypeId', 
      'driveTypeId', 
      'modificationId'
    ]);
  }

  onYearChange(selectedValue: any) {
    this.isGenerationVisible = selectedValue !== null;
    this.isBodyTypeVisible = false;
    this.isTransmissionTypeVisible = false;
    this.isEngineTypeVisible = false;
    this.isDriveTypeVisible = false;
    this.isModificationVisible = false;

    this.resetFields([  
      'generationId', 
      'bodyTypeId', 
      'transmissionTypeId', 
      'engineTypeId', 
      'driveTypeId', 
      'modificationId'
    ]);
  }

  onGenerationChange(selectedValue: any) {
    this.isBodyTypeVisible = selectedValue !== null;
    this.isTransmissionTypeVisible = false;
    this.isEngineTypeVisible = false;
    this.isDriveTypeVisible = false;
    this.isModificationVisible = false;

    this.resetFields([ 
      'bodyTypeId', 
      'transmissionTypeId', 
      'engineTypeId', 
      'driveTypeId', 
      'modificationId'
    ]);
  }

  onBodyTypeChange(selectedValue: any) {
    this.isTransmissionTypeVisible = selectedValue !== null;
    this.isEngineTypeVisible = false;
    this.isDriveTypeVisible = false;
    this.isModificationVisible = false;

    this.resetFields([ 
      'transmissionTypeId', 
      'engineTypeId', 
      'driveTypeId', 
      'modificationId'
    ]);
  }

  onTransmissionTypeChange(selectedValue: any) {
    this.isEngineTypeVisible = selectedValue !== null;
    this.isDriveTypeVisible = false;
    this.isModificationVisible = false;

    this.resetFields([ 
      'engineTypeId', 
      'driveTypeId', 
      'modificationId'
    ]);
  }

  onEngineTypeChange(selectedValue: any) {
    this.isDriveTypeVisible = selectedValue !== null;
    this.isModificationVisible = false;

    this.resetFields([ 
      'driveTypeId', 
      'modificationId'
    ]);
  }

  onDriveTypeChange(selectedValue: any) {
    this.isModificationVisible = selectedValue !== null;

    this.resetFields([ 
      'modificationId'
    ]);
  }

  onPlaceRegionChange(selectedValue: any) {
    this.isPlaceCityVisible = selectedValue !== null;

    this.resetFields([ 
      'placeCityId'
    ]);
  }
  

  ngOnInit(): void {
    this.advertService.getAdvertForm(this.validateForm.value).subscribe({
      next: (data) => {
        this.advertFormData = data;
      }
    });
  
    this.validateForm.valueChanges
      .pipe(
        debounceTime(50),
        distinctUntilChanged((prev, curr) => JSON.stringify(prev) === JSON.stringify(curr))
      )
      .subscribe((formValues) => {
        console.log(formValues);
        this.advertService.getAdvertForm(formValues).subscribe({
          next: (data) => {
            this.advertFormData = data;
          }
        });
      });
  }

  get phones(): FormArray {
    return this.validateForm.controls.Params.get('phones') as FormArray;
  }

  addPhone(): void {
    this.phones.push(this.createPhoneFormGroup());
  }

  removePhone(index: number): void {
    this.phones.removeAt(index);
  }
  
  createPhoneFormGroup(): FormGroup {
    return this.fb.group({
      phoneCodeId: this.fb.control(1),
      number: this.fb.control(null)
    });
  }
  
  submitForm() {
    if (this.validateForm.invalid) {
      console.log('Форма невалидна');
      this.validateForm.markAllAsTouched();
      return;
    }

    const payload = this.validateForm.value;
    console.log('Данные для отправки:', payload);

    this.advertService.createAdvert(payload).subscribe({
      next: () => {
        this.message.success('Объявление успешно создано!');
      },
      error: () => {
        this.message.error('Ошибка при отправке формы.');
      }
    })

  }

}


