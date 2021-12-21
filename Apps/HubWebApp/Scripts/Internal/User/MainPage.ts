import { UserPage } from '@jasonbenfield/sharedwebapp/User/UserPage';
import { Startup } from '@jasonbenfield/sharedwebapp/Startup';

new UserPage(new Startup().build(), null);