import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Question } from '../_models/question';
import { environment } from '../../environments/environment';

@Injectable( {
  providedIn: 'root'
} )
export class QuestionService {
  question: Observable<Question>;
  apiUrl = environment.apiUrl;

  constructor( private http: HttpClient ) {
  }



}
