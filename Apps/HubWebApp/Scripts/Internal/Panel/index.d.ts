import * as ko from 'knockout';

interface IPanel {
    activate();
    deactivate();
}
interface IPanelViewModel {
    readonly isActive: ko.Observable<boolean>;    
}