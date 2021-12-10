//
//  BudgetViewModel.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 10/12/2021.
//

import Foundation

class BudgetViewModel: ObservableObject {
	private let fetcher: BudgetFetcher
	
	init(fetcher: BudgetFetcher) {
		self.fetcher = fetcher
	}
	
}
