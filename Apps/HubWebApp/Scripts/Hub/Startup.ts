import { PageLoader } from './PageLoader';
import { AppApiCollection, setApi } from './AppApiCollection';
import { AppApiEvents } from './AppApiEvents';
import { ConsoleLog } from './ConsoleLog';
import { modalError, setModalError, ModalErrorComponent } from './Error/ModalErrorComponent';
import { ModalErrorComponentViewModel } from './Error/ModalErrorComponentViewModel';
import { PageFrameViewModel } from './PageFrameViewModel';
import { PageViewModel } from './PageViewModel';

export function startup(
    createPageVM: () => any,
    createPage: (vm: PageViewModel) => any
) {
    let modalErrorVM = new ModalErrorComponentViewModel();
    setModalError(new ModalErrorComponent(modalErrorVM));
    let defaultEvents = new AppApiEvents((err) => {
        new ConsoleLog().error(err.toString());
        modalError.show(err.getErrors(), err.getCaption());
    });
    setApi(new AppApiCollection(defaultEvents));
    let pageVM = createPageVM();
    createPage(pageVM);
    let pageFrameVM = new PageFrameViewModel(pageVM, modalErrorVM);
    new PageLoader().load(pageFrameVM);
}