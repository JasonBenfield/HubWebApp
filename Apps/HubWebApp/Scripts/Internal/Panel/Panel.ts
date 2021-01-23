import { PanelViewModel } from "./PanelViewModel";

export class Panel<TContent, TContentVM> {
    private isActive: boolean = false;

    constructor(
        readonly content: TContent,
        private readonly vm: PanelViewModel<TContentVM>
    ) {
    }

    activate() {
        if (!this.isActive) {
            this.isActive = true;
            this.vm.isActive(this.isActive);
        }
    }

    deactivate() {
        if (this.isActive) {
            this.isActive = false;
            this.vm.isActive(this.isActive);
        }
    }
}