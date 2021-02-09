import { Startup } from 'xtistart';
import { PageFrame } from 'XtiShared/PageFrame';

class MainPage {
    constructor(private readonly page: PageFrame) {
    }
}
new MainPage(new Startup().build());