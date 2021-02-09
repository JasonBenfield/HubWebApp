import { HubAppApi } from '../Hub/Api/HubAppApi';
import { BaseStartup } from 'XtiShared/BaseStartup';

export class Startup extends BaseStartup {
    protected getDefaultApi() {
        return HubAppApi;
    }
}