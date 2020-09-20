"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.startup = void 0;
var PageLoader_1 = require("./PageLoader");
var AppApiCollection_1 = require("./AppApiCollection");
var AppApiEvents_1 = require("./AppApiEvents");
var ConsoleLog_1 = require("./ConsoleLog");
var ModalErrorComponent_1 = require("./Error/ModalErrorComponent");
var ModalErrorComponentViewModel_1 = require("./Error/ModalErrorComponentViewModel");
var PageFrameViewModel_1 = require("./PageFrameViewModel");
function startup(createPageVM, createPage) {
    var modalErrorVM = new ModalErrorComponentViewModel_1.ModalErrorComponentViewModel();
    ModalErrorComponent_1.setModalError(new ModalErrorComponent_1.ModalErrorComponent(modalErrorVM));
    var defaultEvents = new AppApiEvents_1.AppApiEvents(function (err) {
        new ConsoleLog_1.ConsoleLog().error(err.toString());
        ModalErrorComponent_1.modalError.show(err.getErrors(), err.getCaption());
    });
    AppApiCollection_1.setApi(new AppApiCollection_1.AppApiCollection(defaultEvents));
    var pageVM = createPageVM();
    createPage(pageVM);
    var pageFrameVM = new PageFrameViewModel_1.PageFrameViewModel(pageVM, modalErrorVM);
    new PageLoader_1.PageLoader().load(pageFrameVM);
}
exports.startup = startup;
//# sourceMappingURL=Startup.js.map