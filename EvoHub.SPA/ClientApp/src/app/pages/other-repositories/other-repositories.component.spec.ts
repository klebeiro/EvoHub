import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OtherRepositoriesComponent } from './other-repositories.component';

describe('OtherRepositoriesComponent', () => {
  let component: OtherRepositoriesComponent;
  let fixture: ComponentFixture<OtherRepositoriesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OtherRepositoriesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OtherRepositoriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
