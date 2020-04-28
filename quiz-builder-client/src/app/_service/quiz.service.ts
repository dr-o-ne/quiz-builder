import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Quiz } from '../_models/quiz';
import { Observable } from 'rxjs';
import { Group } from '../_models/group';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class QuizService{
    group: Observable<Group>;
    apiUrl = environment.apiUrl;

    constructor(private http: HttpClient){}

    getAllQuizzes(): Observable<object> {
        return this.http.get(this.apiUrl + 'quizzes');
    }

    getQuiz(id: number): Observable<object> {
      return this.http.get(this.apiUrl + 'quizzes/' + id);
    }

    createQuiz(quiz: Quiz): Observable<object> {
      return this.http.post(this.apiUrl + 'quizzes', quiz);
    }

    updateQuiz(quiz: Quiz): Observable<object> {
      return this.http.put(this.apiUrl + 'quizzes', quiz);
    }

    getGroupData() {
        return this.http.get('assets/group.json');
    }

    getGroup(id): Observable<Group> {
        const tempListGroup = localStorage.getItem('grouplist');
        const groupList = JSON.parse(tempListGroup);
        // tslint:disable-next-line: radix
        const index = groupList.findIndex((obj => obj.id === Number.parseInt(id)));
        this.group = groupList[index];
        return this.group;
    }
}
