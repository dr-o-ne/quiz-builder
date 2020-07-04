import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class GroupDataProvider {

    apiUrl = environment.apiUrl;

    constructor(private http: HttpClient) {
    }

    reorderGroups(quizId: string, groupIds: string[]): Observable<object> {
        const body = { quizId, groupIds };
        return this.http.put(this.apiUrl + 'groups/reorder', body);
    }

    renameGroup(groupId: string, name: string): Observable<object> {
        const body = { groupId, name }
        return this.http.put(this.apiUrl + 'groups/rename', body);
    }

}
