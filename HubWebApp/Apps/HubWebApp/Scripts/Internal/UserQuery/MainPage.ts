import { BasicPage } from '@jasonbenfield/sharedwebapp/Components/BasicPage';
import { ApiODataClient } from '@jasonbenfield/sharedwebapp/OData/ApiODataClient';
import { ODataComponent } from '@jasonbenfield/sharedwebapp/OData/ODataComponent';
import { ODataComponentOptionsBuilder } from '@jasonbenfield/sharedwebapp/OData/ODataComponentOptionsBuilder';
import { ListGroup } from '@jasonbenfield/sharedwebapp/Components/ListGroup';
import { MessageAlert } from '@jasonbenfield/sharedwebapp/Components/MessageAlert';
import { Url } from '@jasonbenfield/sharedwebapp/Url';
import { TextLinkListGroupItemView } from '@jasonbenfield/sharedwebapp/Views/ListGroup';
import { HubAppApi } from '../../Lib/Api/HubAppApi';
import { ODataExpandedUserColumnsBuilder } from '../../Lib/Api/ODataExpandedUserColumnsBuilder';
import { Apis } from '../Apis';
import { MainPageView } from './MainPageView';
import { UserGroupListItem } from '../UserGroups/UserGroupListItem';
import { ODataCellClickedEventArgs } from '@jasonbenfield/sharedwebapp/OData/ODataCellClickedEventArgs';
import { WebPage } from '@jasonbenfield/sharedwebapp/Api/WebPage';

class MainPage extends BasicPage {
    protected readonly view: MainPageView;
    private readonly hubApi: HubAppApi;
    private readonly alert: MessageAlert;
    private readonly userGroups: ListGroup;
    private readonly odataComponent: ODataComponent<IExpandedUser>;

    constructor() {
        super(new MainPageView());
        this.hubApi = new Apis(this.view.modalError).Hub();
        this.alert = new MessageAlert(this.view.alert);
        this.userGroups = new ListGroup(this.view.userGroups);
        const columns = new ODataExpandedUserColumnsBuilder(this.view.columns);
        columns.UserID.require();
        columns.UserGroupName.require();
        const options = new ODataComponentOptionsBuilder<IExpandedUser>('hub_users', columns);
        options.query.select.addFields(
            columns.UserName,
            columns.PersonName,
            columns.Email
        );
        options.saveChanges();
        const userGroupName = Url.current().getQueryValue('UserGroupName');
        options.setODataClient(
            new ApiODataClient(this.hubApi.UserQuery, { UserGroupName: userGroupName })
        );
        this.odataComponent = new ODataComponent(this.view.odataComponent, options.build());
        this.odataComponent.when.dataCellClicked.then(this.onDataCellClicked.bind(this));
        this.load();
    }

    private onDataCellClicked(eventArgs: ODataCellClickedEventArgs) {
        const userID = eventArgs.record['UserID'];
        const userGroupName = eventArgs.record['UserGroupName'];
        const url = this.hubApi.Users.Index.getModifierUrl(userGroupName, { UserID: userID });
        new WebPage(url).open();
    }

    private async load() {
        const userGroups = await this.getUserGroups();
        userGroups.splice(0, 0, null);
        const userGroupName = Url.current().getQueryValue('UserGroupName') || '';
        this.userGroups.setItems(
            userGroups,
            (ug, itemView: TextLinkListGroupItemView) => {
                const listItem = new UserGroupListItem(this.hubApi, ug, itemView);
                const listItemGroupName = ug ? ug.GroupName.DisplayText : '';
                if (userGroupName === listItemGroupName) {
                    listItem.makeActive();
                }
                return listItem;
            }
        );
        this.odataComponent.refresh();
    }

    private getUserGroups() {
        return this.alert.infoAction(
            'Loading...',
            () => this.hubApi.UserGroups.GetUserGroups()
        );
    }
}
new MainPage();