import * as template from './MainPage.html';
import { PageViewModel } from '../PageViewModel';

export class MainPageViewModel extends PageViewModel {
    constructor() {
        super(template);
    }
}