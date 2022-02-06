import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticleTypesComponent } from './article-types.component';

describe('ArticleTypesComponent', () => {
  let component: ArticleTypesComponent;
  let fixture: ComponentFixture<ArticleTypesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ArticleTypesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ArticleTypesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
