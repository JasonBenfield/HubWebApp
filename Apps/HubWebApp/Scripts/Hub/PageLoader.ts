import * as ko from 'knockout';
import * as template from './Templates/PageFrame.html';
import './Styles/default.scss';
import { ComponentTemplate } from './ComponentTemplate';
import 'tslib';
import { SubmitBindingHandler } from './SubmitBindingHandler';
import { ModalBindingHandler } from './ModalBindingHandler';

export class PageLoader {
    load(pageVM: any) {
        new ComponentTemplate('page-frame', template).register();
        ko.options.deferUpdates = true;
        ko.bindingHandlers.submit = new SubmitBindingHandler();
        ko.bindingHandlers.modal = new ModalBindingHandler();
        ko.applyBindings(pageVM);
    }
}