import { UserPage } from '@jasonbenfield/sharedwebapp/User/UserPage';
import { Startup } from '@jasonbenfield/sharedwebapp/Startup';
import { Apis } from '../../Hub/Apis';

let pageFrame = new Startup().build();
new UserPage(pageFrame, new Apis(pageFrame.modalError).hub());