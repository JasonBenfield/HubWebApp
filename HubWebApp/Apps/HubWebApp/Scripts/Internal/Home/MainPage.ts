import { Startup } from '@jasonbenfield/sharedwebapp/Startup';
import { PageFrameView } from '@jasonbenfield/sharedwebapp/PageFrameView';

class MainPage {
    constructor(private readonly page: PageFrameView) {
    }
}
new MainPage(new Startup().build());