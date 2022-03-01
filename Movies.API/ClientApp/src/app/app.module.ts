import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { MovieListComponent } from './movie-list/movie-list.component';
import { MovieDetailComponent } from './movie-detail/movie-detail.component';
import { AddMovieComponent } from './add-movie/add-movie.component';
import { EditMovieComponent } from './edit-movie/edit-movie.component';
import { MovieService } from './services/movie.service';
import { GenreService } from './services/genre.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    MovieListComponent,
    MovieDetailComponent,
    AddMovieComponent,
    EditMovieComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: MovieListComponent, pathMatch: 'full' },
      { path: 'AddMovie', component: AddMovieComponent },
      { path: 'DetailMovie/:id', component: MovieDetailComponent },
      { path: 'EditMovie/:id', component: EditMovieComponent },
      { path: '**', redirectTo: ''}
    ])
  ],
  providers: [
    MovieService,
    GenreService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
