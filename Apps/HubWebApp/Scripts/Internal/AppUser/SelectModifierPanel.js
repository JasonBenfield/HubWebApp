"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.SelectModifierPanel = exports.SelectModifierPanelResult = void 0;
var tslib_1 = require("tslib");
var Awaitable_1 = require("@jasonbenfield/sharedwebapp/Awaitable");
var Command_1 = require("@jasonbenfield/sharedwebapp/Command/Command");
var DelayedAction_1 = require("@jasonbenfield/sharedwebapp/DelayedAction");
var ListGroup_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroup");
var MessageAlert_1 = require("@jasonbenfield/sharedwebapp/MessageAlert");
var ModifierListItem_1 = require("./ModifierListItem");
var SelectModifierPanelResult = /** @class */ (function () {
    function SelectModifierPanelResult(results) {
        this.results = results;
    }
    SelectModifierPanelResult.back = function () { return new SelectModifierPanelResult({ back: {} }); };
    SelectModifierPanelResult.modifierSelected = function (modifier) {
        return new SelectModifierPanelResult({
            modifierSelected: { modifier: modifier }
        });
    };
    Object.defineProperty(SelectModifierPanelResult.prototype, "back", {
        get: function () { return this.results.back; },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(SelectModifierPanelResult.prototype, "modifierSelected", {
        get: function () {
            return this.results.modifierSelected;
        },
        enumerable: false,
        configurable: true
    });
    return SelectModifierPanelResult;
}());
exports.SelectModifierPanelResult = SelectModifierPanelResult;
var SelectModifierPanel = /** @class */ (function () {
    function SelectModifierPanel(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        this.awaitable = new Awaitable_1.Awaitable();
        this.alert = new MessageAlert_1.MessageAlert(this.view.alert);
        this.modifiers = new ListGroup_1.ListGroup(this.view.modifiers);
        this.modifiers.itemClicked.register(this.onModifierClicked.bind(this));
        new Command_1.Command(this.back.bind(this)).add(this.view.backButton);
    }
    SelectModifierPanel.prototype.back = function () {
        this.awaitable.resolve(SelectModifierPanelResult.back());
    };
    SelectModifierPanel.prototype.setModCategory = function (modCategory) {
        this.modCategory = modCategory;
    };
    SelectModifierPanel.prototype.onModifierClicked = function (item) {
        this.awaitable.resolve(SelectModifierPanelResult.modifierSelected(item.modifier));
    };
    SelectModifierPanel.prototype.start = function () {
        new DelayedAction_1.DelayedAction(this.delayedStart.bind(this), 1).execute();
        return this.awaitable.start();
    };
    SelectModifierPanel.prototype.delayedStart = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var modifiers;
            var _this = this;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return _this.hubApi.ModCategory.GetModifiers(_this.modCategory.ID); })];
                    case 1:
                        modifiers = _a.sent();
                        this.modifiers.setItems(modifiers, function (mod, itemView) {
                            return new ModifierListItem_1.ModifierListItem(mod, itemView);
                        });
                        return [2 /*return*/];
                }
            });
        });
    };
    SelectModifierPanel.prototype.activate = function () { this.view.show(); };
    SelectModifierPanel.prototype.deactivate = function () { this.view.hide(); };
    return SelectModifierPanel;
}());
exports.SelectModifierPanel = SelectModifierPanel;
//# sourceMappingURL=SelectModifierPanel.js.map