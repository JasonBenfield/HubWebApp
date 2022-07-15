import { UserPage } from '@jasonbenfield/sharedwebapp/User/UserPage';
import { Apis } from '../../Apis';

new UserPage((pageView) => new Apis(pageView.modalError).Hub());