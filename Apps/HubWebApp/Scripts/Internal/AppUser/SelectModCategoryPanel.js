"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.SelectModCategoryPanel = exports.SelectModCategoryPanelResult = void 0;
var tslib_1 = require("tslib");
var Awaitable_1 = require("@jasonbenfield/sharedwebapp/Awaitable");
var Command_1 = require("@jasonbenfield/sharedwebapp/Command/Command");
var DelayedAction_1 = require("@jasonbenfield/sharedwebapp/DelayedAction");
var Enumerable_1 = require("@jasonbenfield/sharedwebapp/Enumerable");
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var ListGroup_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroup");
var MessageAlert_1 = require("@jasonbenfield/sharedwebapp/MessageAlert");
var ModCategoryListItem_1 = require("./ModCategoryListItem");
var SelectModCategoryPanelResult = /** @class */ (function () {
    function SelectModCategoryPanelResult(results) {
        this.results = results;
    }
    SelectModCategoryPanelResult.back = function () { return new SelectModCategoryPanelResult({ back: {} }); };
    SelectModCategoryPanelResult.defaultModSelected = function () {
        return new SelectModCategoryPanelResult({ defaultModSelected: {} });
    };
    SelectModCategoryPanelResult.modCategorySelected = function (modCategory) {
        return new SelectModCategoryPanelResult({
            modCategorySelected: { modCategory: modCategory }
        });
    };
    Object.defineProperty(SelectModCategoryPanelResult.prototype, "back", {
        get: function () { return this.results.back; },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(SelectModCategoryPanelResult.prototype, "defaultModSelected", {
        get: function () {
            return this.results.defaultModSelected;
        },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(SelectModCategoryPanelResult.prototype, "modCategorySelected", {
        get: function () {
            return this.results.modCategorySelected;
        },
        enumerable: false,
        configurable: true
    });
    return SelectModCategoryPanelResult;
}());
exports.SelectModCategoryPanelResult = SelectModCategoryPanelResult;
var SelectModCategoryPanel = /** @class */ (function () {
    function SelectModCategoryPanel(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        this.awaitable = new Awaitable_1.Awaitable();
        new TextBlock_1.TextBlock('Modifier Categories', this.view.titleHeader);
        this.alert = new MessageAlert_1.MessageAlert(this.view.alert);
        this.modCategories = new ListGroup_1.ListGroup(this.view.modCategories);
        this.modCategories.itemClicked.register(this.onModCategoryClicked.bind(this));
        new Command_1.Command(this.back.bind(this)).add(this.view.backButton);
    }
    SelectModCategoryPanel.prototype.back = function () {
        this.awaitable.resolve(SelectModCategoryPanelResult.back());
    };
    SelectModCategoryPanel.prototype.onModCategoryClicked = function (item) {
        this.awaitable.resolve(SelectModCategoryPanelResult.modCategorySelected(item.modCategory));
    };
    SelectModCategoryPanel.prototype.start = function () {
        new DelayedAction_1.DelayedAction(this.delayedStart.bind(this), 1).execute();
        return this.awaitable.start();
    };
    SelectModCategoryPanel.prototype.delayedStart = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var modCategories;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return _this.hubApi.App.GetModifierCategories(); })];
                    case 1:
                        modCategories = _a.sent();
                        modCategories = new Enumerable_1.FilteredArray(modCategories, function (mc) { return mc.Name.toLowerCase() !== 'default'; }).value();
                        if (modCategories.length === 0) {
                            this.awaitable.resolve(SelectModCategoryPanelResult.defaultModSelected());
                        }
                        else {
                            this.modCategories.setItems(modCategories, function (mc, itemView) {
                                return new ModCategoryListItem_1.ModCategoryListItem(mc, itemView);
                            });
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    SelectModCategoryPanel.prototype.activate = function () { this.view.show(); };
    SelectModCategoryPanel.prototype.deactivate = function () { this.view.hide(); };
    return SelectModCategoryPanel;
}());
exports.SelectModCategoryPanel = SelectModCategoryPanel;
//# sourceMappingURL=SelectModCategoryPanel.js.map