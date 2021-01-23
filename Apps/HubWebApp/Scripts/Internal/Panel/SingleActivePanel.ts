import { Panel } from "./Panel";
import { PanelViewModel } from "./PanelViewModel";

export class SingleActivePanel {
    private readonly panels: Panel<any, any>[] = [];

    add<T, TVM>(panelVM: PanelViewModel<TVM>, createContent: (vm) => T) {
        let content = createContent(panelVM.content);
        let panel = new Panel<T, TVM>(content, panelVM);
        this.panels.push(panel);
        return panel;
    }

    private _previousActive: Panel<any, any> = null;

    get previousActive() { return this._previousActive; }

    private _currentActive: Panel<any, any> = null;

    get currentActive() { return this._currentActive; }

    activate(panel: Panel<any, any>) {
        this._previousActive = this._currentActive;
        for (let p of this.panels) {
            p.deactivate();
        }
        panel.activate();
        this._currentActive = panel;
    }
}