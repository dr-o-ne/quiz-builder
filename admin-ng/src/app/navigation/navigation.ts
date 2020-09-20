import { FuseNavigation } from '@fuse/types';

export const navigation: FuseNavigation[] = [
    {
        id       : 'dashboards',
        title    : 'Dashboards',
        type     : 'group',
        children : [
            {
                id       : 'quizzes',
                title    : 'Quizzes',
                type     : 'item',
                icon     : 'assignment',
                url      : '/quizzes'
            },
            {
                id       : 'users',
                title    : 'Users',
                type     : 'item',
                icon     : 'account_box',
                url      : '/users'
            }
        ]
    }
];
